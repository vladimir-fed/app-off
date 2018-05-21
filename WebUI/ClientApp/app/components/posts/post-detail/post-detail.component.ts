import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs/Subscription';

import { Post, User } from '../../../shared/shared.models';
import { SharedService } from '../../../shared/shared.service';
import { AuthService } from '../../auth/auth.service';

@Component({
  selector: 'post-detail',
  templateUrl: './post-detail.component.html'
})
export class PostDetailComponent implements OnInit, OnDestroy {
  subs: Subscription;
  post: Post | undefined;
  user: User | undefined;

  constructor(private sharedService: SharedService, private route: ActivatedRoute, public authService: AuthService) { }

  ngOnInit() {
    this.subs = this.route.params.subscribe(params => {
      let id = +params['id'];
      this.post = undefined;
      this.user = undefined;
      this.sharedService.getPost(id).subscribe(result => {
        this.post = result;
        this.sharedService.getUser(this.post.userId).subscribe(u => this.user = u, e => console.error(e))
      }, error => console.error(error));
    });
  }

  ngOnDestroy(){
    this.subs.unsubscribe();
  }

}
