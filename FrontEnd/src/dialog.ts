import { DialogController } from 'aurelia-dialog';
import { autoinject } from 'aurelia-framework';

@autoinject
export class Dialog {
    message?: string;
    okAction?: (args?: any) => {};
    cancelAction?: (args?: any) => {};

    constructor(private dialogController: DialogController) {
        dialogController.settings.centerHorizontalOnly = true;
    }

    activate(model: any) {
        this.message = model.message;
        this.okAction = model.okAction;
        this.cancelAction = model.cancelAction;
    }

    ok(): void {
        this.okAction();
        this.dialogController.ok();
    }

    cancel(): void {
        this.cancelAction();
        this.dialogController.cancel();
    }
}
