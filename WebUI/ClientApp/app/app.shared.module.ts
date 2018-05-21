import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { ErrorPageComponent } from './components/error-page/error-page.component';
import { HomeComponent } from './components/home/home.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { PostsComponent } from './components/posts/posts.component';
import { PostListComponent } from './components/posts/post-list/post-list.component';
import { PostItemComponent } from './components/posts/post-item/post-item.component';
import { PostEditComponent } from './components/posts/post-edit/post-edit.component';
import { PostStartComponent } from './components/posts/post-start/post-start.component';
import { PostDetailComponent } from './components/posts/post-detail/post-detail.component';
import { UsersComponent } from './components/users/users.component';
import { UserDetailsComponent } from './components/user-details/user-details.component';
import { UserPostsComponent } from './components/user-posts/user-posts.component';

import { SharedService } from './shared/shared.service';
import { PostCommentsComponent } from './components/posts/post-comments/post-comments.component';
import { AuthService } from './components/auth/auth.service';
import { AuthGuard } from './components/auth/auth-guard.service';
import { SignupComponent } from './components/auth/signup/signup.component';
import { PostEditGuard } from './components/posts/post-edit/post-edit-guard.service';

@NgModule({
    declarations: [
        AppComponent,
        ErrorPageComponent,
        NavMenuComponent,
        HomeComponent,
        PostsComponent,        
        PostListComponent,
        PostItemComponent,
        PostEditComponent,
        PostStartComponent,
        PostDetailComponent,
        PostCommentsComponent,
        SignupComponent,
        UsersComponent,      
        UserDetailsComponent,
        UserPostsComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'signin', component: SignupComponent, data: { signupMode: false } },
            { path: 'signup', component: SignupComponent, data: { signupMode: true }  },
            { path: 'home', component: HomeComponent },
            { path: 'users', component: UsersComponent },
            { path: 'users/:id', component: UserDetailsComponent },
            { path: 'posts', component: PostsComponent, children: [
                {path: '', component: PostStartComponent },
                {path: 'new', component: PostEditComponent, canActivate: [AuthGuard] },
                {path: ':id', component: PostDetailComponent },
                {path: ':id/edit', component: PostEditComponent, canActivate: [AuthGuard, PostEditGuard] },
            ]},
            { path: 'not-found', component: ErrorPageComponent, data: { message: 'Page not found!' } },
            { path: '**', redirectTo: 'not-found' }
        ])
    ],
    providers: [
        SharedService,
        AuthService,
        AuthGuard,
        PostEditGuard
    ]
})
export class AppModuleShared {
}
