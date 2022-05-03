using UnityEngine;

namespace Examples.CSharp.IEnumerator
{
    
    /// <summary>
    /// reference: https://qiita.com/riekure/items/9f59fec68c3e31f38f11
    ///
    /// yield return null
    /// 1フレーム分処理を中断、次のフレームで続きの行を処理
    ///
    /// yield break
    /// コルーチンを途中で終了、再開はできない
    ///
    /// yield return new WaitForSeconds(float seconds)
    /// 指定した seconds 秒、コルーチンを中断する
    ///
    /// yield return new WaitUntil(Func predicate)
    /// predicate の関数の返り値が true になったら処理を再開する
    ///
    /// yield return new WaitWhile(Func predicate)
    /// predicate の関数の返り値が false になったら処理を再開する
    ///
    /// yield return StartCoroutine()
    /// 指定したコルーチンを実行、完了するまで後続処理は行わない
    ///
    /// StopAllCoroutines()
    /// コルーチンを全て止める
    /// 
    /// </summary>
    public class ExampleIEnumerator : MonoBehaviour
    {

    }
}

