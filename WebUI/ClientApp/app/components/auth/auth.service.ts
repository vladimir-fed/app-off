import { Router } from '@angular/router';
import { Http } from '@angular/http'
import { Injectable } from '@angular/core';

import { TokenModel, User } from '../../shared/shared.models';
import { SignInModel, SignUpModel } from '../../shared/shared.models';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class AuthService {
  tokenModel: TokenModel | null;
  signInModel: SignInModel | null;
  user: User;

  constructor(private httpClient: Http, private router: Router) {
    let tokenFromStorage = localStorage.getItem("tokenModel");
    if (tokenFromStorage){
      this.tokenModel = JSON.parse(tokenFromStorage) as TokenModel;
      this.user = JSON.parse(atob(this.tokenModel.token.split('.')[1])) as User;
    }
  }

  signinUser(signInModel: SignInModel) {
    return this.httpClient.post('/api/signIn', signInModel).map(
      r => {
        this.tokenModel = r.json() as TokenModel;
        this.signInModel = signInModel;  
        this.user = JSON.parse(atob(this.tokenModel.token.split('.')[1])) as User;
        console.log(this.user);        
        
        localStorage.setItem("tokenModel", JSON.stringify(this.tokenModel));

        return true;
      }        
    );
  }

  signupUser(signupUser: SignUpModel): Observable<boolean> {
    return this.httpClient.post('/api/signUp', signupUser).map(
      r => {
        this.tokenModel = r.json() as TokenModel;
        this.signInModel = signupUser as SignInModel;
        this.user = JSON.parse(atob(this.tokenModel.token.split('.')[1])) as User;
        console.log(this.user);        

        localStorage.setItem("tokenModel", JSON.stringify(this.tokenModel));

        return true; 
      }        
    );
  }

  logout() {
    this.tokenModel = null;
    this.signInModel = null;
    this.user = new User();
    localStorage.removeItem("tokenModel");
  }

  getUser(){
    return this.user;
  }

  getToken(): string {
    
    return this.tokenModel ? this.tokenModel.token : '';
  }

  isAuthenticated() {
    return this.tokenModel != null;
  }
}
