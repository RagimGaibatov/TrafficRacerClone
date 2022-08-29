using System.Collections;
using TMPro;
using UnityEngine;

namespace UI{
    public class UIScore : MonoBehaviour{
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI addedScoreText;

        private Score score;
        private Coroutine _coroutine;

        public void Construct(Score score){
            this.score = score;
            this.score.OnChangedScore += UpdateUIScore;
            this.score.OnAddedScore += UpdateUIAddedScore;

            UpdateUIScore();
        }

        private void OnDestroy(){
            this.score.OnChangedScore -= UpdateUIScore;
            this.score.OnAddedScore -= UpdateUIAddedScore;
        }


        void UpdateUIScore(){
            scoreText.text = "Score : " + score.GetScore;
        }

        void UpdateUIAddedScore(int addedScore){
            if (addedScore > 10){
                if (_coroutine != null){
                    StopCoroutine(_coroutine);
                }

                addedScoreText.text = "+ " + addedScore;
                _coroutine = StartCoroutine(SwitchOnAndOffUIAddedScore());
            }
        }

        IEnumerator SwitchOnAndOffUIAddedScore(){
            addedScoreText.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            addedScoreText.gameObject.SetActive(false);
        }
    }
}