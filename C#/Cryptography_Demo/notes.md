Cryptography is used to achieve the following goals:

    Confidentiality: To help protect a user's identity or data from being read.

    Data integrity: To help protect data from being changed.

    Authentication: To ensure that data originates from a particular party.

    Non-repudiation: To prevent a particular party from denying that they sent a message.

4 diff cat:

1. symm key
- same key both end, 
 ip of n bytes into op block, Because n is small (8 bytes for DES and TripleDES; 16 bytes [the default], 24 bytes, or 32 bytes for AES), data values that are larger than n have to be encrypted one block at a time

2. asymm key
- 2 diff key are used
 private key kept secret
 public key known to everyone
 rsa, dsa(digi sign algorithm), edcdifiiehellman


3. digital signing (use hashing internally)
- for authentication and intigirity
working
1. A:msg -> hash() -> msg_digest -> pvt_key -> sign
2. B:pkt -> pub_of_A -> hash() -> check digest_data == hash_msg 

4. hashing
Deterministic — same input -> same output
Fixed-length — regardless of input size
Irreversible — you cannot get the original data back
Unique (ideally) — different data -> different hash

Algorithm	Output Size	Notes
MD5	128 bits	⚠️ Broken — don’t use for security
SHA1	160 bits	⚠️ Weak — avoid for new systems
SHA256	256 bits	✅ Secure and widely used
SHA384	384 bits	✅ Stronger, slower
SHA512	512 bits	✅ Very strong
