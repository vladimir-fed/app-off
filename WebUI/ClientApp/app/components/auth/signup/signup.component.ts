import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { AuthService } from '../auth.service';
import { SignUpModel, SignInModel } from '../../../shared/shared.models';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
})
export class SignupComponent implements OnInit {
  signupMode: boolean;

  constructor(private route: ActivatedRoute, private router: Router,
    private authService: AuthService) { }

  ngOnInit() {
    this.route.data.subscribe(
      data => this.signupMode = data["signupMode"]
    )
  }

  onSubmit(form: NgForm) {
    if (this.signupMode){
      this.authService.signupUser(form.value as SignUpModel).subscribe( r => {
          if (r){
            this.router.navigate(['/']);
          }
      });
    } else{
      this.authService.signinUser(form.value as SignInModel).subscribe( r => {
        if (r){
          this.router.navigate(['/']);
        }
    });
    }
  }
}
