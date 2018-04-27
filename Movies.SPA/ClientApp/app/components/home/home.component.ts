import { Component, OnInit } from '@angular/core';
import { Actor } from '../shared/actor.type';
import { MoviesService } from '../shared/movies.service';

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {

    actors: Actor[];

    constructor(private moviesService: MoviesService) {
    }

    ngOnInit() {
        this.moviesService.getRolesByActor().then(actors => { this.actors = actors });
    }
}
