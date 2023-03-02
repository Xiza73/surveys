import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BranchService } from 'src/app/services/branch.service';
import { QuestionService } from 'src/app/services/question.service';
import { PersonService } from '../../services/person.service';
import { SurveyService } from '../../services/survey.service';

@Component({
  templateUrl: './new-survey.component.html',
  styleUrls: ['./new-survey.component.scss'],
})
export class NewSurveyComponent implements OnInit {
  people: any[] = [];

  questions: any[] = [];

  branches: any[] = [];

  surveyForm: FormGroup = this.fb.group({
    idPerson: [0, Validators.required],
    idBranch: [0, Validators.required],
    question1: ['', Validators.required],
    question2: ['', Validators.required],
    question3: ['', Validators.required],
    question4: ['', Validators.required],
    question5: ['', Validators.required],
    question6: ['', Validators.required],
  });

  constructor(
    private fb: FormBuilder,
    private personService: PersonService,
    private questionService: QuestionService,
    private branchService: BranchService,
    private surveyService: SurveyService
  ) {}

  ngOnInit(): void {
    this.getPeople();

    this.questionService.getQuestions().subscribe(
      (response) => {
        console.log(response);
        this.questions = response;
      },
      (error) => {
        console.log(error);
      }
    );

    this.branchService.getBranches().subscribe(
      (response) => {
        console.log(response);
        this.branches = response;
      },
      (error) => {
        console.log(error);
      }
    );
  }

  getPeople() {
    this.personService.getPeople().subscribe(
      (response) => {
        this.people = response;
      },
      (error) => {
        console.log(error);
      }
    );
  }

  onSubmit() {
    console.log(this.surveyForm.value);
    if (this.surveyForm.invalid) {
      this.surveyForm.markAllAsTouched();
      return;
    }
    const arrQuestions = this.questions.map((question, index) => {
      const type = question.type;
      const value = this.surveyForm.value[this.getQuestionAttribute(index + 1)];
      let percentage = 0;
      if (type === 1) {
        percentage = value === 'Y' ? 100 : 0;
      } else if (type === 2) {
        percentage = value * 10;
      } else if (type === 3) {
        percentage = 50;
      }
      return {
        id: question.idQuestion,
        value,
        percentage,
      };
    });

    const questions = JSON.stringify(arrQuestions);

    const body = {
      idPerson: parseInt(this.surveyForm.value.idPerson),
      idBranch: parseInt(this.surveyForm.value.idBranch),
      questions,
    };

    console.log(body);

    this.surveyService.saveSurvey(body).subscribe(
      (response) => {
        console.log(response);
        alert('Encuesta guardada correctamente');
        this.surveyForm.reset();
      },
      (error) => {
        console.log(error);
        alert('Error al guardar la encuesta');
      }
    );
  }

  getQuestionAttribute(id: number) {
    return 'question' + id;
  }

  isRequiredField(field: string): boolean {
    const formControl = this.surveyForm.get(field);
    return (
      formControl?.errors?.['required'] &&
      formControl?.touched &&
      '*Este campo es requerido'
    );
  }
}
