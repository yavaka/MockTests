// ignore_for_file: avoid_unnecessary_containers, prefer_const_constructors, prefer_const_literals_to_create_immutables

import 'package:flutter/material.dart';
import 'package:mock_tests/ui/components/identity_button.dart';
import 'package:mock_tests/ui/constants.dart';
import 'package:mock_tests/ui/views/Identity/login_page.dart';
import 'package:mock_tests/ui/views/Identity/register_page.dart';

class HomePage extends StatefulWidget {
  static const String id = "home_page";
  const HomePage({super.key});

  @override
  State<HomePage> createState() => _HomePageState();
}

class _HomePageState extends State<HomePage> {
  @override
  Widget build(BuildContext context) {
    return Container(
      color: Colors.white,
      child: Stack(
        children: <Widget>[
          Column(
            crossAxisAlignment: CrossAxisAlignment.stretch,
            mainAxisAlignment: MainAxisAlignment.center,
            children: <Widget>[
              // Title
              Text(
                "Mock Tests",
                textAlign: TextAlign.center,
                style: TextStyle(
                  fontSize: 30,
                  color: kPrimaryLightColor,
                  decoration: TextDecoration.none,
                ),
              ),
              SizedBox(
                height: 150,
              ),

              // Login Button
              IdentityButton(title: 'Login', onPressed: () => Navigator.pushNamed(context, LoginPage.id)),
              SizedBox(
                height: 20,
              ),

              // Register Button
              IdentityButton(title: 'Register', onPressed: () => Navigator.pushNamed(context, RegisterPage.id)),
            ],
          )
        ],
      ),
    );
  }
}
