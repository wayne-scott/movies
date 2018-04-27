import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';

import { Role } from '../../shared/role.type';

@Component({
    selector: 'role-table',
    templateUrl: './role-table.component.html',
    styleUrls: ['./role-table.component.css']
})
export class RoleTableComponent {

    @Input() roles: Role[];

    constructor(private router: Router) {
    }
}
