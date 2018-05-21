import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Post } from '../../shared/shared.models';
import { SharedService } from '../../shared/shared.service';

@Component({
    selector: 'user-posts',
    templateUrl: './user-posts.component.html'
})
export class UserPostsComponent implements OnInit, OnDestroy {
    private sub: any;
    public posts: Post[];

    constructor(private sharedService: SharedService, private route: ActivatedRoute) {
    }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            let id = +params['id'];
            this.sharedService.getAllUserPosts(id).subscribe(result => this.posts = result, error => console.error(error));
        });
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }
}