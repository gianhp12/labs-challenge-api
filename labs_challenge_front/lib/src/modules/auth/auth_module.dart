import 'package:flutter_modular/flutter_modular.dart';
import 'package:labs_challenge_front/src/modules/auth/data/repositories/auth_repository_impl.dart';
import 'package:labs_challenge_front/src/modules/auth/interactor/actions/auth_login_actions.dart';
import 'package:labs_challenge_front/src/modules/auth/interactor/actions/auth_register_actions.dart';
import 'package:labs_challenge_front/src/modules/auth/interactor/actions/auth_validate_token_actions.dart';
import 'package:labs_challenge_front/src/modules/auth/interactor/repositories/auth_repository.dart';
import 'package:labs_challenge_front/src/modules/auth/ui/pages/auth_page.dart';
import 'package:labs_challenge_front/src/modules/auth/ui/pages/register_page.dart';
import 'package:labs_challenge_front/src/modules/auth/ui/pages/validate_token_page.dart';
import 'package:labs_challenge_front/src/shared/states/session_notifier.dart';
import 'package:labs_challenge_front/src/shared_module.dart';

class AuthModule extends Module {
  final session = Modular.get<SessionNotifier>();
  @override
  void binds(Injector i) {
    //ACTIONS
    i.add<AuthLoginActions>(AuthLoginActions.new);
    i.add<AuthRegisterActions>(AuthRegisterActions.new);
    i.add<AuthValidateTokenActions>(AuthValidateTokenActions.new);
    //REPOSITORIES
    i.add<AuthRepository>(AuthRepositoryImpl.new);
  }

  @override
  void routes(RouteManager r) {
    r.child(
      '/',
      child: (context) => const AuthPage(),
      transition: TransitionType.fadeIn,
    );
    r.child(
      '/register',
      child: (context) => const RegisterPage(),
      transition: TransitionType.fadeIn,
    );
    r.child(
      '/validate-token',
      child: (context) {
        final args = r.args.data as Map<String, dynamic>;
        final email = args['email'];
        final password = args['password'];
        return ValidateTokenPage(email: email, password: password);
      },
      transition: TransitionType.fadeIn,
    );
  }

  @override
  List<Module> get imports => [SharedModule()];
}
