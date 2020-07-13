import { RouterConfiguration, Router } from 'aurelia-router';
import { PLATFORM } from "aurelia-framework";
export class App {
    router: Router;
    configureRouter(config: RouterConfiguration, router: Router): void {
        this.router = router;
        config.title = 'MyApp';
        config.options.root = '/';
        config.options.pushState = true;
        config.map([
            { route: ['', 'cadastrar'], name: 'cadastrar', moduleId: PLATFORM.moduleName('produtos/cadastrar') },
            { route: 'details/*id', name: 'details', moduleId: PLATFORM.moduleName('produtos/details'), nav: true, title: 'details', href: "#details" }
        ]);
    }
}
