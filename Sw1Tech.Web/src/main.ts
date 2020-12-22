import { Aurelia, PLATFORM } from 'aurelia-framework'
import environment from './environment';
import { AuthService } from './services/AuthService';

export function configure(aurelia: Aurelia) {
    aurelia.use
        .standardConfiguration()
        .feature('resources')
        .plugin('aurelia-animator-css')
        .plugin(PLATFORM.moduleName('aurelia-validation'))
        .plugin(PLATFORM.moduleName('aurelia-event-aggregator'));
            
    if (environment.debug) {
        aurelia.use.developmentLogging();
    }

    if (environment.testing) {
        aurelia.use.plugin('aurelia-testing');
    }

    aurelia.start().then(() => {
        let auth = aurelia.container.get(AuthService);
        let root = auth.DoAuthenticado() ? PLATFORM.moduleName('app')
            : PLATFORM.moduleName('usuario/login');

        aurelia.setRoot(root);
    });
}
