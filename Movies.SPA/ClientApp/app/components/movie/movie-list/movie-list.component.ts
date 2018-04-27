
import { Component, OnInit } from '@angular/core';
import { MoviesService } from '../../shared/movies.service';

@Component({
    selector: 'movie-list',
    templateUrl: './movie-list.component.html',
    styleUrls: ['./movie-list.component.css']
})
export class MovieListComponent implements OnInit {

    movies: string[];

    constructor(private moviesService: MoviesService) {
    }

    ngOnInit(): void {
        this.moviesService.getMovies().then(movies => { this.movies = movies });
    }
}
