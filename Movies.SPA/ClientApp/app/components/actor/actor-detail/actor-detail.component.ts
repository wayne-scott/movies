
import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Actor } from '../../shared/actor.type';
import { MoviesService } from '../../shared/movies.service';

@Component({
    selector: 'actor-detail',
    templateUrl: './actor-detail.component.html'
})
export class ActorDetailComponent implements OnInit {

    @Input() actor: Actor;

    constructor(private moviesService: MoviesService,
                private activatedRoute: ActivatedRoute) {
    }

    ngOnInit() {
        if (this.actor) {
            return;
        }

        let actorName = this.activatedRoute.snapshot.params['actorName'] as string;
        this.moviesService.getActor(actorName).then(actor => { this.actor = actor });        
    }
}    