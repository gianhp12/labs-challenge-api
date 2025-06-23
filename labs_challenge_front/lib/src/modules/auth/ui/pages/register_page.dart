import 'package:flutter/material.dart';
import 'package:flutter_modular/flutter_modular.dart';
import 'package:labs_challenge_front/src/modules/auth/interactor/actions/auth_register_actions.dart';
import 'package:labs_challenge_front/src/modules/auth/interactor/states/auth_register_state.dart';
import 'package:labs_challenge_front/src/modules/auth/ui/widgets/auth_container.dart';
import 'package:labs_challenge_front/src/modules/auth/ui/widgets/password_strength_indicator.dart';
import 'package:labs_challenge_front/src/shared/hooks/use_state.dart';
import 'package:labs_challenge_front/src/shared/utils/form_validators.dart';
import 'package:labs_challenge_front/src/shared/widgets/app_button.dart';
import 'package:labs_challenge_front/src/shared/widgets/app_text_form_field.dart';

class RegisterPage extends StatefulWidget {
  const RegisterPage({super.key});

  @override
  State<RegisterPage> createState() => _RegisterPageState();
}

class _RegisterPageState extends State<RegisterPage> with UseState{
  final _actions = Modular.get<AuthRegisterActions>();
  final _formKey = GlobalKey<FormState>();
  final _nameController = TextEditingController();
  final _emailController = TextEditingController();
  final _passwordController = TextEditingController();
  final _nameFocus = FocusNode();
  final _emailFocus = FocusNode();
  final _passwordFocus = FocusNode();
  String passwordStrengthText = '';

  @override
  void initState() {
    super.initState();
     _actions.addListener(() {
      final state = _actions.currentState;
      if (state is SuccessAuthRegisterState) {
        Modular.to.pushNamed<String?>(
          './validate-token',
          arguments: {'loggedUser': state.loggedUser},
        );
      }
      setState(() {});
    });
    _passwordFocus.addListener(() {
      if (!_passwordFocus.hasFocus) {
        setState(() {
          passwordStrengthText = '';
        });
      }
    });
  }

  @override
  void dispose() {
    _nameController.dispose();
    _emailController.dispose();
    _passwordController.dispose();
    _nameFocus.dispose();
    _emailFocus.dispose();
    _passwordFocus.dispose();
    _passwordFocus.removeListener(() {});
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    final state = _actions.currentState;
    final isLoading = state is LoadingAuthRegisterState;
    final errorMessage = (state is ErrorAuthRegisterState) ? state.exception : null;
    return Scaffold(
      backgroundColor: Colors.blue.shade50,
      body: Center(
        child: AuthContainer(
          showBackButton: true,
          subtitle: "Crie sua conta para começar",
          child: Form(
            key: _formKey,
            child: Column(
              mainAxisSize: MainAxisSize.min,
              children: [
                AppTextFormField(
                  controller: _nameController,
                  label: "Nome",
                  icon: Icons.person_outline,
                  focusNode: _nameFocus,
                  autovalidateMode: AutovalidateMode.onUserInteraction,
                  validator:
                      (value) =>
                          FormValidators.nameValidator(value),
                ),
                const SizedBox(height: 20),
                AppTextFormField(
                  controller: _emailController,
                  label: "E-mail",
                  icon: Icons.email_outlined,
                  focusNode: _emailFocus,
                  validator: (value) => FormValidators.isEmail(value),
                  autovalidateMode: AutovalidateMode.onUserInteraction,
                ),
                const SizedBox(height: 20),
                AppTextFormField(
                  controller: _passwordController,
                  label: "Senha",
                  icon: Icons.lock_outline,
                  focusNode: _passwordFocus,
                  obscureText: true,
                  autovalidateMode: AutovalidateMode.onUserInteraction,
                  validator:
                      (value) =>
                          FormValidators.passwordStrengthValidator(value),
                  onChanged: (value) {
                    setState(() {
                      passwordStrengthText =
                          FormValidators.passwordStrengthLevel(value);
                    });
                  },
                ),
                const SizedBox(height: 6),
                PasswordStrengthIndicator(strengthText: passwordStrengthText),
                const SizedBox(height: 20),
                SizedBox(
                  width: double.infinity,
                  child: AppButton(
                    label: 'Cadastrar',
                    isLoading: isLoading,
                    backgroundColor: Colors.blue.shade700,
                    onPressed: () async {
                      if (_formKey.currentState!.validate()) {
                        final username = _nameController.text;
                        final email = _emailController.text;
                        final password = _passwordController.text;
                        await _actions.register(username, email, password);
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
              ],
            ),
          ),
        ),
      ),
    );
  }
}
