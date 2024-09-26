﻿using System.Windows;

namespace DichotomyMethod
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Graph Graph { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Graph = new Graph();
        }

        private void buttonSolve_Click(object sender, RoutedEventArgs e)
        {
            Graph.GetFunction(this, tbFunction.Text);
        }

        private void buttonDichotomy_Solve_Click(object sender, RoutedEventArgs e)
        {
            Graph.DichotomyMethod(this);
        }
    }
}