// See https://aka.ms/new-console-template for more information
using HelloWorld_01;
using System;

Console.WriteLine("Hello, World!");

var ag = new MyAggregate();

ag.Apply(new object[]{ "Hello", " ", "world", DateTime.Now});
