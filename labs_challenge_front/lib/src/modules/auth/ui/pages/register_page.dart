import 'package:flutter/material.dart';
import 'package:flutter_modular/flutter_modular.dart';
import 'package:labs_challenge_front/src/modules/auth/ui/widgets/back_to_home_button.dart';
import 'package:labs_challenge_front/src/modules/auth/ui/widgets/password_strength_indicator.dart';
import 'package:labs_challenge_front/src/shared/utils/form_validators.dart';
import 'package:labs_challenge_front/src/shared/widgets/app_button.dart';
import 'package:labs_challenge_front/src/shared/widgets/app_text_form_field.dart';

class RegisterPage extends StatefulWidget {
  const RegisterPage({super.key});

  @override
  State<RegisterPage> createState() => _RegisterPageState();
}

class _RegisterPageState extends State<RegisterPage> {
  final _formKey = GlobalKey<FormState>();
  final _nameController = TextEditingController();
  final _emailController = TextEditingController();
  final _passwordController = TextEditingController();

  final _nameFocus = FocusNode();
  final _emailFocus = FocusNode();
  final _passwordFocus = FocusNode();

  bool isLoading = false;
  String? errorMessage;
  String passwordStrengthText = '';

  @override
  void initState() {
    super.initState();
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
    return Scaffold(
      backgroundColor: Colors.blue.shade50,
      body: Center(
        child: Container(
          constraints: const BoxConstraints(maxWidth: 450),
          padding: const EdgeInsets.all(32),
          decoration: BoxDecoration(
            color: Colors.white,
            borderRadius: BorderRadius.circular(16),
            boxShadow: const [
              BoxShadow(
                color: Colors.black26,
                blurRadius: 20,
                offset: Offset(0, 10),
              ),
            ],
          ),
          child: Form(
            key: _formKey,
            child: Column(
              mainAxisSize: MainAxisSize.min,
              children: [
                const BackToHomeButton(),
                const Icon(Icons.local_shipping, size: 64, color: Colors.blue),
                const SizedBox(height: 10),
                const Text(
                  "Logistica App",
                  style: TextStyle(
                    fontSize: 26,
                    fontWeight: FontWeight.bold,
                    color: Colors.black87,
                  ),
                ),
                const SizedBox(height: 10),
                const Text(
                  "Crie sua conta para começar",
                  style: TextStyle(fontSize: 16, color: Colors.black54),
                ),
                const SizedBox(height: 24),
                AppTextFormField(
                  controller: _nameController,
                  label: "Nome",
                  icon: Icons.person_outline,
                  focusNode: _nameFocus,
                  autovalidateMode: AutovalidateMode.onUserInteraction,
                  validator:
                      (value) =>
                          FormValidators.isRequired(value, fieldName: 'Nome'),
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
                    onPressed: () {
                      if (_formKey.currentState!.validate()) {
                        Modular.to.pushNamed('/validate-token');
                      }
                    },
                  ),
                ),
                if (errorMessage != null) ...[
                  const SizedBox(height: 10),
                  Text(
                    errorMessage!,
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
