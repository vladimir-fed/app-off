import { Component } from '@angular/core';
import { User } from '../../shared/shared.models';
import { SharedService } from '../../shared/shared.service';

@Component({
    selector: 'users',
    templateUrl: './users.component.html'
})
export class UsersComponent {
    public users: User[];

    constructor(private sharedService: SharedService) {
        this.sharedService.getAllUsers().subscribe(result => this.users = result, error => console.error(error));
    }
}
