import { assert } from 'chai';
import { FormsModule } from '@angular/forms';
import { ValuesComponent } from './values.component';
import { TestBed, async, ComponentFixture } from '@angular/core/testing';
import { HttpModule, Http, Response, ResponseOptions, XHRBackend } from '@angular/http';

let fixture: ComponentFixture<ValuesComponent>;

describe('Values component', () => {
	beforeEach(() => {
		TestBed.configureTestingModule({
			imports: [HttpModule, FormsModule],
			declarations: [ValuesComponent]
		});
		fixture = TestBed.createComponent(ValuesComponent);
		fixture.detectChanges();
	});

	it('should display a title', async(() => {
		const titleText = fixture.nativeElement.querySelector('h1').textContent;
		expect(titleText).toEqual('Values List/Search');
	}));
});
