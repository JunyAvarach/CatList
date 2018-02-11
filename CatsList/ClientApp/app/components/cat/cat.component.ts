import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';


@Component({
    selector: 'cat',
    templateUrl: './cat.component.html'
})

export class PersonDataComponent {
    public cats: Cat[];
    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {

        http.get('api/cat')
            .subscribe(result => {
                this.cats = result.json() as Cat[];
            }, error => console.error(error));
    }
}
interface Cat {
    name: string;
    ownerGender: string;
}