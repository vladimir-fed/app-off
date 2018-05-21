import { Component, OnInit } from '@angular/core'
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { Post, User } from '../../../shared/shared.models';
import { SharedService } from '../../../shared/shared.service';
import { AuthService } from '../../auth/auth.service';

@Component({
    selector: 'post-edit',
    templateUrl: './post-edit.component.html',
    styleUrls: [ './post-edit.component.scss']
})
export class PostEditComponent implements OnInit {
    id: number;
    editMode: boolean;
    postForm: FormGroup;
    users: User[];

    constructor(private sharedService: SharedService, 
        private route: ActivatedRoute,
        private authService: AuthService,
    private router: Router) { }

    ngOnInit() {
        this.route.params.subscribe(
            params => {
                this.editMode = params['id'] != null;
                this.id = +params["id"];

                this.initForm();
            }
        );
        this.sharedService.getAllUsers().subscribe(u => this.users = u);
    }

    initForm() {
        if (this.editMode) {
            this.sharedService.getPost(this.id).subscribe(
                p => {
                    this.postForm = new FormGroup({
                        "title": new FormControl(p.title, Validators.required),
                        "content": new FormControl(p.content, Validators.required)
                    });
                });
        } else {
            this.postForm = new FormGroup({
                "title": new FormControl(null, Validators.required),
                "content": new FormControl(null, Validators.required)
            });
        }
    }

    onSubmit() {
        let post =<Post> this.postForm.value;

        if (this.editMode){
            post.id = this.id;
            post.userId = this.authService.getUser().id;
            this.sharedService.updatePost(this.id, post);
        } else {
            post.userId = this.authService.getUser().id;
            this.sharedService.createPost(post);
        }
        this.onCancel();
    }

    onDelete(){        
        this.sharedService.deletePost(this.id);
        this.router.navigate(["/posts"]);        
    }

    onCancel(){
        this.router.navigate([".."], {relativeTo: this.route});
    }

}