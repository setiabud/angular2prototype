import { Injectable, Inject, Component } from '@angular/core';

// import http for ajax calls
import { Http } from '@angular/http'; 

@Component({
	selector: 'weather',
	template: require('./weather.component.html')
})

@Injectable()
export class WeatherComponent {
	public weather: Weather;
	private apiUrl;

	constructor(private http: Http) {
		this.apiUrl = "/api/weather/";
	}

	public getWeather(chosenCity: string) {
		var action = 'city/';

		// ajax get and bind result to weather class
		this.http.get(this.apiUrl + action + chosenCity).subscribe(result => {
			this.weather = result.json();
		});
	}
}

interface Weather {
	temp: string;
	summary: string;
	city: string;
}