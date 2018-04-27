
import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/Rx';

import { Actor } from './actor.type'
import { Movie } from './movie.type';

@Injectable()
export class MoviesService {
    
    constructor(private http: Http) {
    }

    getRolesByActor(): Promise<Actor[]> {
        return this.http.get('http://localhost:57371/api/actor/role')
            .toPromise()
            .then(response => response.json() as Actor[])
            .catch(this.handleError)
    }

    getActor(actorName: string): Promise<Actor> {
        return this.http.get(`http://localhost:57371/api/actor/${actorName}`)
            .toPromise()
            .then(response => response.json() as Actor)
            .catch(this.handleError)
    }

    getActors(): Promise<string[]> {
        return this.http.get(`http://localhost:57371/api/actor`)
            .toPromise()
            .then(response => response.json() as string[])
            .catch(this.handleError)
    }

    getMovies(): Promise<string[]> {
        return this.http.get(`http://localhost:57371/api/movie`)
            .toPromise()
            .then(response => response.json() as string[])
            .catch(this.handleError)
    }

    getMovie(movieName: string): Promise<Movie> {
        return this.http.get(`http://localhost:57371/api/movie/${movieName}`)
            .toPromise()
            .then(response => response.json() as Movie)
            .catch(this.handleError)
    }

    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error);
        return Promise.reject(error.message || error);
    }
}
