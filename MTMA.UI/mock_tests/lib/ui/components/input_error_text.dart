import 'package:flutter/material.dart';

class InputErrorText extends StatefulWidget {
  const InputErrorText({
    super.key,
    required this.errorText,
  });

  final String? errorText;

  @override
  State<InputErrorText> createState() => _InputErrorTextState();
}

class _InputErrorTextState extends State<InputErrorText> {
  @override
  Widget build(BuildContext context) {
    return Visibility(
      visible: widget.errorText != null,
      child: Text(
        widget.errorText ?? '',
        style: const TextStyle(
          fontSize: 12,
          color: Colors.red,
        ),
      ),
    );
  }
}
