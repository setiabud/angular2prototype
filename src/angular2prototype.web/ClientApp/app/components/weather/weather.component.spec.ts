//describe('Meaningful Test', () => {
//	it('1 + 1 => 2', () => {
//		expect(1 + 1).toBe(2);
//	});

//	it('always fails', () => {
//		expect(0).toBe(1);
//	});
//});


import { assert } from 'chai';
import { WeatherComponent } from './weather.component';
import { TestBed, async, ComponentFixture } from '@angular/core/testing';

let fixture: ComponentFixture<WeatherComponent>;

describe('Counter component', () => {
	beforeEach(() => {
		TestBed.configureTestingModule({ declarations: [WeatherComponent] });
		fixture = TestBed.createComponent(WeatherComponent);
		fixture.detectChanges();
	});

	it('should display a title', async(() => {
		const titleText = fixture.nativeElement.querySelector('h1').textContent;
		expect(titleText).toEqual('Weather Check');
	}));
});
