import { assert } from 'chai';
import { FormsModule } from '@angular/forms';
import { ValuesComponent } from './values.component';
import { TestBed, async, ComponentFixture, inject } from '@angular/core/testing';
import { HttpModule, Http, Response, ResponseOptions, XHRBackend } from '@angular/http';
import { MockBackend, MockConnection } from '@angular/http/testing';

let fixture: ComponentFixture<ValuesComponent>;

describe('Values component', () => {
	beforeEach(() => {
		TestBed.configureTestingModule({
			imports: [HttpModule, FormsModule],
			providers: [
				ValuesComponent,
				{ provide: XHRBackend, useClass: MockBackend }
			],
			declarations: [ValuesComponent]
		});
		fixture = TestBed.createComponent(ValuesComponent);
		fixture.detectChanges();
	});

	it('should display a title', async(() => {
		const titleText = fixture.nativeElement.querySelector('h1').textContent;
		expect(titleText).toEqual('Values List/Search');
	}));

	it('should call search and return results',
		async(inject([ValuesComponent, XHRBackend], (valuesComponent: ValuesComponent, mockBackend) => {

			const mockResponse = [
				{ id: 11, name: 'Name Eleven' },
				{ id: 12, name: 'Name Twelve' },
				{ id: 13, name: 'Name Thirteen' }
			];

			mockBackend.connections.subscribe((connection: MockConnection) => {
				connection.mockRespond(new Response(new ResponseOptions({
					body: JSON.stringify(mockResponse)
				})));
			});

			valuesComponent.searchValues('Name');
			expect(valuesComponent.values.length).toBe(3);
			expect(valuesComponent.values[0].name).toEqual('Name Eleven');
			expect(valuesComponent.values[1].name).toEqual('Name Twelve');
			expect(valuesComponent.values[2].name).toEqual('Name Thirteen');
		}))
	);
});
