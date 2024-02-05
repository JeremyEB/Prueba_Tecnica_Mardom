import { Component } from '@angular/core';
import { CommonModule, NgOptimizedImage } from '@angular/common';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faEnvelope } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    CommonModule, FontAwesomeModule, NgOptimizedImage
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  gmail = 'Prueba Tecnica: jeremyeb002@gmail.com'
  faEnvelope = faEnvelope;
}
