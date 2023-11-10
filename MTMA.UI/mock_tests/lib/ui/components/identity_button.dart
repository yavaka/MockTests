// ignore_for_file: prefer_const_constructors, must_be_immutable

import 'package:flutter/material.dart';
import 'package:mock_tests/core/common/constants.dart';

class IdentityButton extends StatefulWidget {
  String title = '';
  final VoidCallback onPressed;

  IdentityButton({
    super.key,
    required this.title,
    required this.onPressed,
  });

  @override
  State<IdentityButton> createState() => _IdentityButtonState();
}

class _IdentityButtonState extends State<IdentityButton> {
  @override
  Widget build(BuildContext context) {
    return Container(
      margin: EdgeInsets.symmetric(horizontal: 20),
      decoration: BoxDecoration(
        borderRadius: BorderRadius.circular(60),
        color: kPrimaryDarkColor,
      ),
      child: TextButton(
        style: TextButton.styleFrom(
          minimumSize: Size(double.infinity, 60),
          backgroundColor: kPrimaryDarkColor,
        ),
        onPressed: widget.onPressed,
        child: Row(
          mainAxisAlignment: MainAxisAlignment.spaceEvenly,
          children: [
            Text(
              widget.title,
              style: TextStyle(
                color: Color(0xFFFFFFFF),
                fontSize: 20,
              ),
            ),
          ],
        ),
      ),
    );
  }
}
