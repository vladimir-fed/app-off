import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Injectable } from '@angular/core';

import { AuthService } from '../../auth/auth.service';
import { Observable } from 'rxjs/Observable';
import { SharedService } from '../../../shared/shared.service';

@Injectable()
export class PostEditGuard implements CanActivate {

  constructor(private authService: AuthService, private sharedService: SharedService) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | boolean {
    let postId = +route.params["id"];
    if (!this.authService.isAuthenticated()) {
        return false;
    }
    
    return this.sharedService.getPost(postId).map(
        p => this.authService.getUser().id === p.userId
    );
  }
}
