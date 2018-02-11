import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { PersonDataComponent} from './components/Cat/Cat.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        PersonDataComponent,
        HomeComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'person', component: PersonDataComponent },
            { path: '**', redirectTo: 'home' }
           // ,{ path: 'sort', component: SortGridPipeComponent }
        ])
    ]
})
export class AppModuleShared {
}
