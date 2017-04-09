import { Component } from '@angular/core';

// import http for ajax calls
import { Http } from '@angular/http'; 

@Component({
	selector: 'values',
	template: require('./values.component.html')
})
export class ValuesComponent {
	public values: Value[];

	constructor(private http: Http) {
	}

	public searchValues(queryName: string) {
		// url to the api address
		var url = '/api/values';

		// ajax get and bind result to weather class
		this.http.get(url + '?name=' + queryName).subscribe(result => {
			this.values = result.json() as Value[];
		});
	}
}

interface Value {
	id: string;
	name: string;
}