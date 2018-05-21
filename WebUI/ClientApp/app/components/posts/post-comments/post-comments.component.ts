import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs/Subscription';

import { Comment } from '../../../shared/shared.models';
import { SharedService } from '../../../shared/shared.service';

@Component({
  selector: 'post-comments',
  templateUrl: './post-comments.component.html'
})
export class PostCommentsComponent implements OnInit, OnDestroy {
  subs: Subscription;
  id: number;
  comments: Comment[];

  constructor(private sharedService: SharedService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.subs = this.route.params.subscribe(params => {
      let id = +params['id'];
      if (this.id !== id) {
        this.id = id;
        this.sharedService.getAllPostComments(id).subscribe(result => {
          this.comments = result;
        }, error => console.error(error));
      }
    });
  }

  ngOnDestroy(){
    this.subs.unsubscribe();
  }

}
