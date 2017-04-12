import { assert } from 'chai';
import { FormsModule } from '@angular/forms';
import { WeatherComponent } from './weather.component';
import { TestBed, async, ComponentFixture, inject } from '@angular/core/testing';
import { HttpModule, Http, Response, ResponseOptions, XHRBackend } from '@angular/http';
import { MockBackend, MockConnection } from '@angular/http/testing';

let comp: WeatherComponent;
let fixture: ComponentFixture<WeatherComponent>;

describe('Weather Component', () => {
	beforeEach(async(() => {
		TestBed.configureTestingModule({
			imports: [HttpModule, FormsModule],
			providers: [
				WeatherComponent,
				{ provide: XHRBackend, useClass: MockBackend }
			],
			declarations: [WeatherComponent]
		});
		fixture = TestBed.createComponent(WeatherComponent);
		fixture.detectChanges();
	}));

	it('should display a title', async(() => {
		const titleText = fixture.nativeElement.querySelector('h1').textContent;
		expect(titleText).toEqual('Weather Check');
	}));

	it('should set weather correctly',
		async(inject([WeatherComponent, XHRBackend], (weatherComponent: WeatherComponent, mockBackend) => {

			const mockResponse = {
				temp: '72', summary: 'Cloudy', city: 'Columbus'
			};

			mockBackend.connections.subscribe((connection: MockConnection) => {
				connection.mockRespond(new Response(new ResponseOptions({
					body: JSON.stringify(mockResponse)
				})));
			});

			weatherComponent.getWeather('Columbus');
			expect(weatherComponent.weather.temp).toEqual('72');
			expect(weatherComponent.weather.summary).toEqual('Cloudy');
			expect(weatherComponent.weather.city).toEqual('Columbus');
		}))
	);
});
