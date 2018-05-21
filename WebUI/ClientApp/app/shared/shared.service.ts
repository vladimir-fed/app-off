import { Inject, Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { Observable } from "rxjs/Observable";
import 'rxjs/Rx';

import { User, Post, Comment } from './shared.models'
import { Subject } from 'rxjs/Subject';
import { AuthService } from '../components/auth/auth.service';

@Injectable()
export class SharedService {
    postsChanged: Subject<Post[]> = new Subject<Post[]>();

    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string, private authService: AuthService) {
    }

    public updatePost(id:number, post: Post){
        let headers = new Headers();
        headers.append("Authorization", "Bearer " + this.authService.getToken());

        this.http.put(this.baseUrl + 'api/posts/' + id.toString(), post, { headers: headers }).subscribe(
            response => this.onPostsChanged()
        );
    }

    public createPost(post: Post){
        let headers = new Headers();
        headers.append("Authorization", "Bearer " + this.authService.getToken());

        this.http.post(this.baseUrl + 'api/posts', post, { headers: headers }).subscribe(
            response => this.onPostsChanged()
        );
    }

    public deletePost(id: number){
        let headers = new Headers();
        headers.append("Authorization", "Bearer " + this.authService.getToken());

        this.http.delete(this.baseUrl + 'api/posts/' + id.toString(), { headers: headers }).subscribe(
            response => this.onPostsChanged()
        );
    }

    public onPostsChanged(){
        this.getAllPosts().subscribe(posts => this.postsChanged.next(posts));
    }

    public getAllUsers(): Observable<User[]> {
        return this.http.get(this.baseUrl + 'api/users').map(r => { return r.json() as User[] });
    }

    public getUser(id:number): Observable<User> {
        return this.http.get(this.baseUrl + 'api/users/' + id).map(r => { return r.json() as User });
    }

    public getPost(id:number): Observable<Post> {
        return this.http.get(this.baseUrl + 'api/posts/' + id).map(r => { return r.json() as Post });
    }

    public getAllPosts(): Observable<Post[]> {
        let headers = new Headers();
        headers.append("Authorization", "Bearer " + this.authService.getToken());

        return this.http.get(this.baseUrl + 'api/posts', { headers: headers }).map(r => { return r.json() as Post[] });
    }

    public getAllUserPosts(id: number): Observable<Post[]> {
        return this.http.get(this.baseUrl + 'api/users/' + id + '/posts').map(r => { return r.json() as Post[] });
    }

    public getAllPostComments(id: number): Observable<Comment[]> {
        return this.http.get(this.baseUrl + 'api/posts/' + id + '/comments').map(r => { return r.json() as Comment[] });
    }
}
