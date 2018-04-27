
import { Component, OnInit } from '@angular/core';
import { MoviesService } from '../../shared/movies.service';

@Component({
    selector: 'actor-list',
    templateUrl: './actor-list.component.html',
    styleUrls: ['./actor-list.component.css']
})
export class ActorListComponent implements OnInit {
    
    actors: string[];

    constructor(private moviesService: MoviesService) {
    }

    ngOnInit(): void {
        this.moviesService.getActors().then(actors => { this.actors = actors });
    }
}
