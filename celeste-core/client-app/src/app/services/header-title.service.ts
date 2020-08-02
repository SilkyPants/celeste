import { Injectable } from "@angular/core";
import { BehaviorSubject } from 'rxjs';

@Injectable()
export class HeaderTitleService {
    title = new BehaviorSubject('Celeste')

    setTitle(title: string) {
        this.title.next(title)
    }
}