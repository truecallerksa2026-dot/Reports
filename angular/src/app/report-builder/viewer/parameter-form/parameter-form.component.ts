import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { ReportParameterDto, ParameterDataType } from '../../models/report.models';

@Component({
  selector: 'app-parameter-form',
  templateUrl: './parameter-form.component.html',
  standalone: false,
})
export class ParameterFormComponent implements OnInit {
  @Input() parameters: ReportParameterDto[] = [];
  @Output() apply = new EventEmitter<Record<string, any>>();

  ParameterDataType = ParameterDataType;
  values: Record<string, any> = {};

  ngOnInit(): void {
    this.parameters.forEach(p => {
      this.values[p.parameterName.replace('@', '')] = p.defaultValue ?? null;
    });
  }

  onApply(): void {
    this.apply.emit({ ...this.values });
  }
}
