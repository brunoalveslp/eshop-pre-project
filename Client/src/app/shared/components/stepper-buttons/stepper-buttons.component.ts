import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-stepper-buttons',
  templateUrl: './stepper-buttons.component.html',
  styleUrls: ['./stepper-buttons.component.scss']
})
export class StepperButtonsComponent {
  @Input() previous: string;
  @Input() next: string;
}
