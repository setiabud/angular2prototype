import { assert } from 'chai';
import { ValuesComponent } from './values.component';
import { TestBed, async, ComponentFixture } from '@angular/core/testing';

let fixture: ComponentFixture<ValuesComponent>;

describe('Values component', () => {
	beforeEach(() => {
		TestBed.configureTestingModule({ declarations: [ValuesComponent] });
		fixture = TestBed.createComponent(ValuesComponent);
		fixture.detectChanges();
	});

	it('should display a title', async(() => {
		const titleText = fixture.nativeElement.querySelector('h1').textContent;
		expect(titleText).toEqual('Values List/Search');
	}));
});
