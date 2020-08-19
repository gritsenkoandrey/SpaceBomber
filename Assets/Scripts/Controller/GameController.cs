﻿using UnityEngine;


namespace Assets.Scripts.Controller
{
    public sealed class GameController : MonoBehaviour
    {
        private Controllers _controllers;

        private void Start()
        {
            _controllers = new Controllers();
            _controllers.Initialization();
        }

        private void Update()
        {
            for (int i = 0; i < _controllers.Length; i++)
            {
                _controllers[i].Execute();
            }
        }
    }
}