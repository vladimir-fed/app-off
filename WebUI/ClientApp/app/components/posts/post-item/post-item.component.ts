import { Component, Input } from '@angular/core';

import { Post } from '../../../shared/shared.models';
import { SharedService } from '../../../shared/shared.service';

@Component({
    selector: 'post-item',
    templateUrl: './post-item.component.html'
})
export class PostItemComponent {
    @Input() post: Post;

    constructor() {    }
}