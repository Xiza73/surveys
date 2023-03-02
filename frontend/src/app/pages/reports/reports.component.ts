import { Component, OnInit } from '@angular/core';
import { BranchService } from '../../services/branch.service';
import { SurveyService } from '../../services/survey.service';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.scss'],
})
export class ReportsComponent implements OnInit {
  branches: any[] = [];

  surveys: any[] = [];

  selectedBranchId: number = 0;

  constructor(
    private branchService: BranchService,
    private surveyService: SurveyService
  ) {}

  ngOnInit() {
    this.branchService.getBranches().subscribe((data) => {
      this.branches = data;

      if (this.branches.length > 0) {
        this.selectedBranchId = this.branches[0].idBranch;
        this.getSurveysByBranchId(this.selectedBranchId);
      }
    });
  }

  getSurveysByBranchId(branchId: number) {
    this.surveyService.getSurveysByBranchId(branchId).subscribe((data) => {
      this.surveys = data;
    });
  }

  onBranchChange(value: number) {
    console.log(value)
    this.selectedBranchId = value;
    this.getSurveysByBranchId(value);
  }
}
