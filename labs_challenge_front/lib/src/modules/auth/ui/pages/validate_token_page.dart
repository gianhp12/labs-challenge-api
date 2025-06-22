import 'package:flutter/material.dart';
import 'package:flutter_modular/flutter_modular.dart';
import 'package:labs_challenge_front/src/shared/utils/form_validators.dart';
import 'package:labs_challenge_front/src/shared/widgets/app_button.dart';
import 'package:labs_challenge_front/src/shared/widgets/app_text_form_field.dart';

class ValidateTokenPage extends StatefulWidget {
  const ValidateTokenPage({super.key});

  @override
  State<ValidateTokenPage> createState() => _ValidateTokenPageState();
}

class _ValidateTokenPageState extends State<ValidateTokenPage> {
  final _formKey = GlobalKey<FormState>();
  final _tokenController = TextEditingController();
  final _tokenFocus = FocusNode();

  bool isLoading = false;
  String? errorMessage;

  @override
  void initState() {
    super.initState();
  }

  @override
  void dispose() {
    _tokenController.dispose();
    _tokenFocus.dispose();
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
                const Icon(Icons.local_shipping, size: 64, color: Colors.blue),
                const SizedBox(height: 20),
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
                  "Informe o token de confirmação de cadastro",
                  style: TextStyle(fontSize: 16, color: Colors.black54),
                ),
                const SizedBox(height: 24),
                AppTextFormField(
                  controller: _tokenController,
                  label: "Token",
                  icon: Icons.person_outline,
                  focusNode: _tokenFocus,
                  autovalidateMode: AutovalidateMode.onUserInteraction,
                  validator:
                      (value) =>
                          FormValidators.isRequired(value, fieldName: 'Token'),
                ),
                const SizedBox(height: 20),
                SizedBox(
                  width: double.infinity,
                  child: AppButton(
                    label: 'Validar',
                    isLoading: isLoading,
                    backgroundColor: Colors.blue.shade700,
                    onPressed: () {
                      if (_formKey.currentState!.validate()) {
                        Modular.to.navigate('/home');
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