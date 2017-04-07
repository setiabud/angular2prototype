import { Component } from '@angular/core';

// import http for ajax calls
import { Http } from '@angular/http'; 

@Component({
	selector: 'weather',
	template: require('./weather.component.html')
})
export class WeatherComponent {
	public weather: Weather;

	constructor(private http: Http) {
	}

	public getWeather(chosenCity: string) {
		// url to the api address
		var url = '/api/weather/city/';

		// ajax get and bind result to weather class
		this.http.get(url + chosenCity).subscribe(result => {
			this.weather = result.json();
		});
	}
}

interface Weather {
	temp: string;
	summary: string;
	city: string;
}