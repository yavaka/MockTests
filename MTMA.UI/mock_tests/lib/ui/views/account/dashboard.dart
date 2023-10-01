// ignore_for_file: unnecessary_this, must_be_immutable

import 'package:flutter/material.dart';
import 'package:jwt_decoder/jwt_decoder.dart';

class Dashboard extends StatefulWidget {
  static String id = 'Dashboard';
  String token;

  Dashboard({super.key, required this.token});

  @override
  State<Dashboard> createState() => _DashboardState();
}

class _DashboardState extends State<Dashboard> {
  late String _email;

  @override
  initState() {
    super.initState();

    var tokenData = JwtDecoder.decode(widget.token);
    this._email = tokenData['unique_name'];
  }

  @override
  Widget build(BuildContext context) {
    return Center(child: Text(this._email));
  }
}
