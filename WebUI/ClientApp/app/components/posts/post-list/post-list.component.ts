import { Component } from '@angular/core';

import { Post } from '../../../shared/shared.models';
import { SharedService } from '../../../shared/shared.service';

@Component({
    selector: 'post-list',
    templateUrl: './post-list.component.html'
})
export class PostListComponent {
    public posts: Post[];

    constructor(private sharedService: SharedService) {
        this.sharedService.getAllPosts().subscribe(result => this.posts = result, error => console.error(error));
        this.sharedService.postsChanged.subscribe(
            posts => this.posts = posts
        );
    }
}