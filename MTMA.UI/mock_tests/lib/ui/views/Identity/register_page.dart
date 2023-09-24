// ignore_for_file: use_build_context_synchronously, prefer_const_constructors

import 'package:flutter/material.dart';
import 'package:mock_tests/core/service_models/identity/register_user_service_model.dart';
import 'package:mock_tests/locator.dart';
import 'package:mock_tests/ui/components/identity_button.dart';
import 'package:mock_tests/ui/components/input_error_text.dart';
import 'package:mock_tests/ui/controllers/identity_controller.dart';
import 'package:mock_tests/ui/views/Identity/login_page.dart';

class RegisterPage extends StatefulWidget {
  static const String id = "register_page";

  const RegisterPage({super.key});

  @override
  State<RegisterPage> createState() => _RegisterPageState();
}

class _RegisterPageState extends State<RegisterPage> {
  final _identityController = locator<IdentityController>();
  final GlobalKey<FormState> _registerFormKey = GlobalKey<FormState>();
  final TextEditingController _usernameController = TextEditingController();
  final TextEditingController _emailController = TextEditingController();
  final TextEditingController _passwordController = TextEditingController();
  final TextEditingController _confirmPasswordController = TextEditingController();
  final ScrollController _scrollController = ScrollController();
  bool _revealPassword = true;

  String? usernameErrorText;
  String? emailErrorText;
  String? passwordErrorText;
  String? confirmPasswordErrorText;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SafeArea(
        // Form
        child: Form(
          key: _registerFormKey,
          child: SingleChildScrollView(
            controller: _scrollController,
            child: Padding(
              padding: const EdgeInsets.all(30.0),
              child: Column(
                mainAxisAlignment: MainAxisAlignment.start,
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  const SizedBox(height: 60),
                  Center(
                    child: const Text(
                      "Enter your details",
                      style: TextStyle(
                        fontSize: 20,
                        fontWeight: FontWeight.w600,
                      ),
                    ),
                  ),

                  // Username Input -------------------------------------
                  const SizedBox(height: 60),
                  TextFormField(
                    controller: _usernameController,
                    decoration: const InputDecoration(
                      hintText: "user name",
                    ),
                  ),
                  const SizedBox(height: 10),
                  InputErrorText(errorText: usernameErrorText),

                  // Email Input -------------------------------------
                  const SizedBox(height: 20),
                  TextFormField(
                    controller: _emailController,
                    decoration: const InputDecoration(
                      hintText: "email addresss",
                    ),
                  ),
                  const SizedBox(height: 10),
                  InputErrorText(errorText: emailErrorText),

                  // Password Input -------------------------------------
                  const SizedBox(height: 20),
                  TextFormField(
                    controller: _passwordController,
                    obscureText: _revealPassword,
                    decoration: InputDecoration(
                      hintText: "password",
                      suffixIcon: GestureDetector(
                        onTap: () {
                          _revealPassword = !_revealPassword;
                          setState(() {});
                        },
                        child: Icon(
                          _revealPassword //
                              ? Icons.visibility_off_outlined
                              : Icons.visibility_outlined,
                        ),
                      ),
                    ),
                  ),
                  const SizedBox(height: 10),
                  InputErrorText(errorText: passwordErrorText),

                  // Confirm Password Input -------------------------------------
                  const SizedBox(height: 20),
                  TextFormField(
                    controller: _confirmPasswordController,
                    obscureText: _revealPassword,
                    decoration: const InputDecoration(
                      hintText: "confirm password",
                    ),
                  ),
                  const SizedBox(height: 10),
                  InputErrorText(errorText: confirmPasswordErrorText),

                  // Register Button ----------------------------------
                  const SizedBox(height: 120),
                  IdentityButton(title: 'Register', onPressed: registerButtonPressed),
                ],
              ),
            ),
          ),
        ),
      ),
    );
  }

  void registerButtonPressed() async {
    if (_registerFormKey.currentState!.validate()) {
      var result = await _identityController.register(
        RegisterUserServiceModel(
          username: _usernameController.text,
          email: _emailController.text,
          password: _passwordController.text,
          confirmPassword: _confirmPasswordController.text,
        ),
      );

      if (result.succeeded) {
        Navigator.pushNamed(context, LoginPage.id);
      } else {
        setState(() {
          if (result.usernameErrors != null) {
            usernameErrorText = result.usernameErrors!.join('\n');
          } else {
            usernameErrorText = null;
          }
          if (result.emailErrors != null) {
            emailErrorText = result.emailErrors!.join('\n');
          } else {
            emailErrorText = null;
          }
          if (result.passwordErrors != null) {
            passwordErrorText = result.passwordErrors!.join('\n');
          } else {
            passwordErrorText = null;
          }
          if (result.confirmPasswordErrors != null) {
            confirmPasswordErrorText = result.confirmPasswordErrors!.join('\n');
          } else {
            confirmPasswordErrorText = null;
          }
          _scrollController.jumpTo(_scrollController.position.maxScrollExtent);
        });
      }
    }
  }

  // textControllers exits when finished
  @override
  void dispose() {
    _usernameController.dispose();
    _emailController.dispose();
    _passwordController.dispose();
    _confirmPasswordController.dispose();
    super.dispose();
  }
}
