using System;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ScoreScreen : MonoBehaviour, IDisposable
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _text;
        private ScoreHandler _scoreHandler;
        private PlayerModule _playerModule;

        private void Start()
        {
            _button.onClick.AddListener(() =>
            {
                _playerModule?.Start();
                _button.gameObject.SetActive(false);
            });
        }

        private void OnDestroy()
        {
            Dispose();
        }


        public void Bind(ScoreHandler scoreHandler, PlayerModule playerModule)
        {
            _playerModule = playerModule;
            _scoreHandler = scoreHandler;
            playerModule.CanStartIsChange += OnCanStartIsChange;
            _scoreHandler.ScoreChanged += OnScoreChanged;
        }

        private void OnScoreChanged()
        {
            _text.text = $"Score: {_scoreHandler.Score}";
        }

        private void OnCanStartIsChange()
        {
            _button.gameObject.SetActive(_playerModule.CanStart);
        }

        public void Dispose()
        {
            if (_playerModule != null)
            {
                _playerModule.CanStartIsChange -= OnCanStartIsChange;
                _scoreHandler.ScoreChanged -= OnScoreChanged;
            }
        }
    }
}