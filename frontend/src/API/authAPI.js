import { mockUsers } from '@/mocks/users'

export async function onLogin(login, password) {
  let user = mockUsers.find((element) => element.login === login && element.password === password)

  if (user) {
    return {
      status: 200,
      data: {
        success: true,
        message: 'Login successful',
        token: '123',
        user: {
          id: user.id,
          login: user.login,
          password: user.password,
        },
      },
    }
  }

  return {
    status: 401,
    data: {
      success: false,
      message: 'Invalid login or password',
    },
  }
}

export async function onLogout() {
  return {
    status: 200,
    data: {
      success: true,
      message: 'Logout successful',
    },
  }
}

export async function getUserByToken(token) {
  if (!token) {
    return {
      status: 401,
      data: {
        success: false,
        message: 'No token provided',
      },
    }
  }

  if (token === '123') {
    return {
      status: 200,
      data: {
        success: true,
        user: {
          id: 3,
          login: 'dhelfy',
          password: 'dhelfy',
        },
      },
    }
  }

  return {
    status: 401,
    data: {
      success: false,
      message: 'Invalid or expired token',
    },
  }
}
