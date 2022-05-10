#[no_mangle]
pub extern "C" fn foo(value: u8) -> u8 {
    (((value as u16) + 1) % 255) as u8
}

#[cfg(test)]
mod tests {
    use super::foo;

    #[test]
    fn test_foo() {
        let a = foo(1);
        let b = foo(254);
        let c = foo(127);
        assert_eq!(a, 2);
        assert_eq!(b, 0);
        assert_eq!(c, 128);
    }
}
