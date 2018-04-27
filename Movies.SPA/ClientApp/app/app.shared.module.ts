import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { HeaderComponent } from './components/shared/header/header.component';
import { HomeComponent } from './components/home/home.component';
import { ActorListComponent } from './components/actor/actor-list/actor-list.component';
import { ActorDetailComponent } from './components/actor/actor-detail/actor-detail.component'
import { RoleTableComponent } from './components/actor/role-table/role-table.component'
import { MovieListComponent } from './components/movie/movie-list/movie-list.component'
import { MovieDetailComponent } from './components/movie/movie-detail/movie-detail.component'

import { MoviesService } from './components/shared/movies.service'

@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
        HeaderComponent,
        ActorListComponent,
        ActorDetailComponent,
        RoleTableComponent,
        MovieListComponent,
        MovieDetailComponent,
    ],
    imports: [
        CommonModule,
        HttpModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'actor', component: ActorListComponent },
            { path: 'actor/:actorName', component: ActorDetailComponent },
            { path: 'movie', component: MovieListComponent },
            { path: 'movie/:movieName', component: MovieDetailComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [ MoviesService ]
})
export class AppModuleShared {
}
