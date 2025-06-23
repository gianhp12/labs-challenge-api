import 'package:flutter/material.dart';
import 'package:flutter_modular/flutter_modular.dart';
import 'package:labs_challenge_front/src/modules/auth/interactor/actions/auth_login_actions.dart';
import 'package:labs_challenge_front/src/modules/auth/interactor/states/auth_login_state.dart';
import 'package:labs_challenge_front/src/modules/auth/ui/widgets/auth_container.dart';
import 'package:labs_challenge_front/src/shared/states/session_notifier.dart';
import 'package:labs_challenge_front/src/shared/utils/form_validators.dart';
import 'package:labs_challenge_front/src/shared/widgets/app_button.dart';
import 'package:labs_challenge_front/src/shared/widgets/app_text_form_field.dart';

class AuthPage extends StatefulWidget {
  const AuthPage({super.key});

  @override
  State<AuthPage> createState() => _AuthPageState();
}

class _AuthPageState extends State<AuthPage> {
  final _formKey = GlobalKey<FormState>();
  final _emailController = TextEditingController();
  final _passwordController = TextEditingController();
  final _emailFocus = FocusNode();
  final _passwordFocus = FocusNode();
  final _actions = Modular.get<AuthLoginActions>();

  @override
  void initState() {
    super.initState();
    final session = Modular.get<SessionNotifier>();
    final message = session.sessionMessage;
    if (message != null) {
      WidgetsBinding.instance.addPostFrameCallback((_) {
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(
            content: Row(
              children: [
                const Icon(Icons.info, color: Colors.black),
                const SizedBox(width: 8),
                Expanded(
                  child: Text(
                    message,
                    style: const TextStyle(color: Colors.black),
                  ),
                ),
              ],
            ),
            backgroundColor: Colors.yellow,
            behavior: SnackBarBehavior.floating,
            shape: RoundedRectangleBorder(
              borderRadius: BorderRadius.circular(12),
            ),
            duration: const Duration(seconds: 3),
            elevation: 6,
          ),
        );
        session.clearSessionMessage();
      });
    }
    _actions.addListener(() {
      final state = _actions.currentState;
      if (state is GettedAuthLoginState) {
        if (state.loggedUser!.isEmailConfirmed) {
          return Modular.to.navigate('/home');
        }
        Modular.to.pushNamed<String?>(
          './validate-token',
          arguments: {'email': state.loggedUser!.email},
        );
      }
      setState(() {});
    });

    void clearError() {
      final current = _actions.currentState;
      if (current is ErrorAuthLoginState) {
        _actions.reset();
      }
    }

    _emailFocus.addListener(() {
      if (_emailFocus.hasFocus) {
        clearError();
      }
    });

    _passwordFocus.addListener(() {
      if (_passwordFocus.hasFocus) {
        clearError();
      }
    });
  }

  @override
  void dispose() {
    _emailController.dispose();
    _passwordController.dispose();
    _emailFocus.dispose();
    _passwordFocus.dispose();
    _actions.removeListener(() {});
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    final state = _actions.currentState;
    final isLoading = state is LoadingAuthLoginState;
    final errorMessage =
        (state is ErrorAuthLoginState) ? state.exception : null;

    return Scaffold(
      backgroundColor: Colors.blue.shade50,
      body: Center(
        child: AuthContainer(
          showBackButton: false,
          subtitle: "Faça login para continuar",
          child: Form(
            key: _formKey,
            child: Column(
              mainAxisSize: MainAxisSize.min,
              children: [
                AppTextFormField(
                  controller: _emailController,
                  label: "E-mail",
                  icon: Icons.email_outlined,
                  focusNode: _emailFocus,
                  validator: (value) => FormValidators.isEmail(value),
                ),
                const SizedBox(height: 20),
                AppTextFormField(
                  controller: _passwordController,
                  label: "Senha",
                  icon: Icons.lock_outline,
                  focusNode: _passwordFocus,
                  obscureText: true,
                  validator: (value) => FormValidators.isRequired(value),
                ),
                const SizedBox(height: 20),
                SizedBox(
                  width: double.infinity,
                  child: AppButton(
                    label: 'Entrar',
                    isLoading: isLoading,
                    backgroundColor: Colors.blue.shade700,
                    onPressed: () async {
                      if (_formKey.currentState!.validate()) {
                        final email = _emailController.text;
                        final password = _passwordController.text;
                        await _actions.login(email, password);
                      }
                    },
                  ),
                ),
                if (errorMessage != null) ...[
                  const SizedBox(height: 10),
                  Text(
                    errorMessage,
                    style: const TextStyle(color: Colors.red, fontSize: 14),
                  ),
                ],
                const SizedBox(height: 10),
                Row(
                  children: [
                    const Expanded(child: Divider()),
                    Padding(
                      padding: const EdgeInsets.symmetric(horizontal: 8),
                      child: Text(
                        "ou",
                        style: TextStyle(color: Colors.grey.shade600),
                      ),
                    ),
                    const Expanded(child: Divider()),
                  ],
                ),
                const SizedBox(height: 10),
                SizedBox(
                  width: double.infinity,
                  child: AppButton(
                    label: 'Criar uma conta',
                    onPressed: () {
                      Modular.to.pushNamed('/register');
                    },
                    type: AppButtonType.outlined,
                    borderColor: Colors.blue.shade700,
                    textColor: Colors.blue.shade700,
                    padding: const EdgeInsets.symmetric(vertical: 16),
                  ),
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }
}
