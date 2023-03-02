import { Component, Input  } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PersonService } from '../../../../services/person.service';

@Component({
  selector: 'app-create-person-modal',
  templateUrl: './create-person-modal.component.html',
  styleUrls: ['./create-person-modal.component.scss'],
})
export class CreatePersonModalComponent {
  @Input() getPeople: () => void = () => {};

  showModal = false;

  registerForm: FormGroup = this.fb.group({
    identification: [
      '',
      [Validators.required, Validators.minLength(10), Validators.maxLength(10)],
    ],
    name: ['', [Validators.required, Validators.maxLength(50)]],
    sex: ['', Validators.required],
    age: [null, [Validators.required, Validators.min(14), Validators.max(100)]],
  });

  constructor(private fb: FormBuilder, private personService: PersonService) {}

  onSubmit() {
    console.log(this.registerForm.value);
    if (this.registerForm.invalid) {
      this.registerForm.markAllAsTouched();
      return;
    }
    this.personService.createPerson(this.registerForm.value).subscribe(
      (response) => {
        console.log(response);
        alert('Persona creada con éxito');
        this.getPeople();
        this.toggleModal();
      },
      (error) => {
        console.log(error);
        alert('Error al crear la persona');
      }
    );
  }

  toggleModal() {
    this.showModal = !this.showModal;
  }

  isRequiredField(field: string): boolean {
    const formControl = this.registerForm.get(field);
    return (
      formControl?.errors?.['required'] &&
      formControl?.touched &&
      '*Este campo es requerido'
    );
  }

  isMinLength(field: string): boolean {
    const formControl = this.registerForm.get(field);
    return (
      formControl?.errors?.['minlength'] &&
      formControl?.touched &&
      '*Este campo debe tener al menos 10 caracteres'
    );
  }

  isMaxLength(field: string, cuantity: number): boolean {
    const formControl = this.registerForm.get(field);
    return (
      formControl?.errors?.['maxlength'] &&
      formControl?.touched &&
      `*Este campo debe tener máximo ${cuantity} caracteres`
    );
  }

  isMin(field: string): boolean {
    const formControl = this.registerForm.get(field);
    return (
      formControl?.errors?.['min'] &&
      formControl?.touched &&
      '*Este campo debe tener un valor mínimo de 14'
    );
  }

  isMax(field: string): boolean {
    const formControl = this.registerForm.get(field);
    return (
      formControl?.errors?.['max'] &&
      formControl?.touched &&
      '*Este campo debe tener un valor máximo de 100'
    );
  }
}
