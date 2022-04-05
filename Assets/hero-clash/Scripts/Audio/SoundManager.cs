using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public List<AudioSource> sources;

    private AudioDataCollection collection;
    public Dictionary<string, AudioData> AudioDict = new Dictionary<string, AudioData>() { };

    private bool _muted;
    private bool _paused;
    private float _timer;

    private void Awake()
    {
        if (SoundManager.Instance == null)
            SoundManager.Instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(gameObject);

        collection = Resources.Load<AudioDataCollection>("AudioData/AudioDataCollection");

        foreach (AudioData audio in collection.GetCollection())
        {
            AudioDict.Add(audio.AudioName, audio);
        }

        _timer = 0;
        _paused = false;
    }

    /// <summary>
    /// Mutes all sources
    /// </summary>
    public void SetMute(bool mute)
    {
        foreach (AudioSource source in sources)
        {
            source.mute = mute;
        }
    }

    /// <summary>
    /// Pauses all sources
    /// </summary>
    public void PauseSources()
    {
        foreach (AudioSource source in sources)
        {
            source.Pause();
        }
        _paused = true;
    }

    /// <summary>
    /// Resumes all sources
    /// </summary>
    public void ResumeSources()
    {
        if (_paused)
        {
            foreach (AudioSource source in sources)
            {
                source.UnPause();
            }
            _paused = false;
        }
    }

    /// <summary>
    /// The standard Play function, plays a clip
    /// </summary>
    public void Play(string audioName)
    {
        AudioSource source = PrepareSource(audioName);
        if (!_paused)
        {
            source.loop = false;
            source.Play();
        }
    }

    /// <summary>
    /// Plays sound every x second
    /// </summary>
    public void Play(string audioName, float time)
    {
        _timer += Time.deltaTime;
        if (_timer >= time)
        {
            Play(audioName);
            _timer = 0;
        }
    }

    /// <summary>
    /// Takes another boolean parameter for looping
    /// </summary>
    public void Play(string audioName, bool loop)
    {
        AudioSource source = PrepareSource(audioName);

        source.loop = loop;

        source.Play();
    }

    /// <summary>
    /// Checks looping and duplicates, if duplicate clip is playing doesn't play
    /// </summary>
    public void Play(string audioName, bool loop, bool unique)
    {
        if (unique && !CheckDuplicateClip(audioName))
        {
            AudioSource source = PrepareSource(audioName);

            if (loop) source.loop = true;

            source.Play();
        }
    }

    /// <summary>
    /// Returns true if there is a duplicate clip playing
    /// </summary>
    private bool CheckDuplicateClip(string sound)
    {
        foreach (AudioSource source in sources)
        {
            if (source.clip.name == sound && source.isPlaying)
                return true;
        }
        return false;
    }

    /// <summary>
    /// Creates and prepares a source, used in the Play method
    /// </summary>
    private AudioSource PrepareSource(string audioName)
    {
        AudioData audio = AudioDict[audioName];
        AudioSource source = GetSource();
        source.clip = audio.GetClip();
        source.volume = audio.volume;
        if (!audio.randomPitch)
            source.pitch = audio.pitch;
        else
            source.pitch = UnityEngine.Random.Range(0.9f, 1.3f);

        return source;
    }

    /// <summary>
    /// Stops the clip
    /// </summary>
    public void Stop(string clipName)
    {
        AudioClip clip = AudioDict[clipName].GetClip();
        foreach (AudioSource source in sources)
        {
            if (source != null)
            {
                if (source.clip == clip && source.isPlaying)
                {
                    source.Stop();
                }
            }

        }
    }

    /// <summary>
    /// Stops all sources
    /// </summary>
    public void StopAll()
    {
        foreach (AudioSource source in sources)
        {
            if (source != null && source.isPlaying) source.Stop();
        }
    }

    /// <summary>
    /// This returns the unused source if there is any, if not creates a new one
    /// </summary>
    /// <returns> Either unused or new AudioSource </returns>
    private AudioSource GetSource()
    {
        foreach (AudioSource source in sources)
        {
            if (source != null)
            {
                if (!source.isPlaying)
                {
                    source.loop = false;
                    return source;
                }
            }
        }
        return CreateSource();
    }

    /// <summary>
    /// Creates AudioSource
    /// </summary>
    /// <returns> New AudioSource </returns>
    private AudioSource CreateSource()
    {
        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        sources.Add(newSource);
        return newSource;
    }

    /// <summary>
    /// This destroys the unused sources whenever needed, for ex: scene changes
    /// Aims for optimizing the amount of audio sources
    /// </summary>
    public void DestroySources()
    {
        List<AudioSource> unusedSources = new List<AudioSource>();
        foreach (AudioSource source in sources)
        {
            if (!source.isPlaying)
            {
                unusedSources.Add(source);
            }
        }
        foreach (AudioSource unusedSource in unusedSources)
        {
            Debug.Log("Destroying: " + unusedSource.clip.name);
            sources.Remove(unusedSource);
            Destroy(unusedSource);
        }
    }

    public void ClearAllSources()
    {
        sources.Clear();
    }
}
