
import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Movie } from '../../shared/movie.type';
import { MoviesService } from '../../shared/movies.service';

@Component({
    selector: 'movie-detail',
    templateUrl: './movie-detail.component.html'
})

export class MovieDetailComponent implements OnInit {

    @Input() movie: Movie

    constructor(private moviesService: MoviesService,
                private activatedRoute: ActivatedRoute) {
    }

    ngOnInit() {
        if (this.movie) {
            return;
        }

        let movieName = this.activatedRoute.snapshot.params['movieName'] as string;
        this.moviesService.getMovie(movieName).then(movie => { this.movie = movie });
    }
}
